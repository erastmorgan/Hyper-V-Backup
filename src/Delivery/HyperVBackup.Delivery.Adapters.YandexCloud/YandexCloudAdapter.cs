using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyperVBackup.Data.FileProgressChange;
using HyperVBackup.Delivery.Abstract;
using HyperVBackup.Delivery.Abstract.Interfaces;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;

namespace HyperVBackup.Delivery.Adapters.YandexCloud
{
    public class YandexCloudAdapter : BaseDeliveryAdapter, IDeliveryAdapter
    {
        #region implementation.

        public Task Configure(IDictionary<string, string> config)
        {
            if (config.TryGetValue(YandexCloudSettingsKey.AccessToken, out var accessToken))
            {
                Settings.Instance.AccessToken = accessToken;
            }

            if (config.TryGetValue(YandexCloudSettingsKey.RefreshToken, out var refreshToken))
            {
                Settings.Instance.RefreshToken = refreshToken;
            }

            return Task.CompletedTask;
        }

        public async Task Upload(
            Stream sourceStream,
            string destinationPath,
            bool overwrite = true,
            CancellationToken cancellationToken = default,
            IProgress<SizeChangeStatus> progress = null)
        {
            using (var api = new DiskHttpApi(Settings.Instance.AccessToken))
            {
                CancellationTokenSource progressChangeCancelTokenSource = null;
                Task progressChangeTask = null;

                if (progress != null)
                {
                    progressChangeCancelTokenSource = new CancellationTokenSource();

                    progressChangeTask = Task.Factory.StartNew(() =>
                    {
                        var totalDataSize = new FileSize(sourceStream.Length);

                        while (!progressChangeCancelTokenSource.Token.IsCancellationRequested)
                        {
                            var currentDataSize = new FileSize(sourceStream.Position);
                            progress.Report(new SizeChangeStatus(totalDataSize, currentDataSize));

                            Thread.Sleep(100);
                        }
                    }, progressChangeCancelTokenSource.Token);
                }

                try
                {
                    await api.Files.UploadFileAsync(destinationPath, overwrite, sourceStream, cancellationToken);
                }
                finally
                {
                    if (progressChangeCancelTokenSource != null)
                    {
                        progressChangeCancelTokenSource.Cancel();
                        progressChangeTask.Wait();
                        progressChangeCancelTokenSource.Dispose();
                    }
                }
            }
        }

        public async Task Download(
            string sourcePath,
            Stream destinationStream,
            CancellationToken cancellationToken = default, 
            IProgress<SizeChangeStatus> progress = null)
        {
            using (var api = new DiskHttpApi(Settings.Instance.AccessToken))
            {
                var remoteStream = await api.Files.DownloadFileAsync(sourcePath, cancellationToken);
                await Copy(remoteStream, destinationStream, cancellationToken, progress);
            }
        }

        #endregion

        #region private methods.

        #endregion
    }
}
