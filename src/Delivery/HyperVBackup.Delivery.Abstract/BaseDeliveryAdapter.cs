using HyperVBackup.Data.FileProgressChange;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HyperVBackup.Delivery.Abstract
{
    public abstract class BaseDeliveryAdapter
    {
        protected async Task Copy(
            Stream sourceStream,
            Stream destinationStream,
            CancellationToken cancellationToken = default,
            IProgress<SizeChangeStatus> progress = null)
        {
            var buffer = new byte[8192];
            var readBytes = buffer.Length;

            var totalDataSize = new FileSize(sourceStream.Length);

            while (readBytes == buffer.Length)
            {
                readBytes = await sourceStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                await destinationStream.WriteAsync(buffer, 0, readBytes, cancellationToken);

                if (progress != null)
                {
                    var currentDataSize = new FileSize(destinationStream.Length);
                    progress.Report(new SizeChangeStatus(totalDataSize, currentDataSize));
                }
            }

            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
