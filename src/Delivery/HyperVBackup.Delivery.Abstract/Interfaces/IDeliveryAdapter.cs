using HyperVBackup.Data.FileProgressChange;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HyperVBackup.Delivery.Abstract.Interfaces
{
    /// <summary>
    /// Defines common interface of stores.
    /// </summary>
    public interface IDeliveryAdapter
    {
        /// <summary>
        /// Configure the store.
        /// </summary>
        /// <param name="config">The key/value configuration data.</param>
        Task Configure(IDictionary<string, string> config);

        /// <summary>
        /// Upload the local file to remote store.
        /// </summary>
        /// <param name="sourceStream">The local file to upload.</param>
        /// <param name="destinationPath">The destination path of store.</param>
        /// <param name="overwrite">When true, an existing remote file will be rewrite with current file.</param>
        /// <param name="cancellationToken">The cancellation token of the current operation.</param>
        /// <param name="progress">The progress change of upload.</param>
        Task Upload(
            Stream sourceStream,
            string destinationPath,
            bool overwrite = true,
            CancellationToken cancellationToken = default,
            IProgress<SizeChangeStatus> progress = null);

        /// <summary>
        /// Download the file from remote store.
        /// </summary>
        /// <param name="sourcePath">The remote file path.</param>
        /// <param name="destinationStream">The local store of downloaded file.</param>
        /// <param name="cancellationToken">The cancellation token of the current operation.</param>
        /// <param name="progress">The progress change of download.</param>
        Task Download(
            string sourcePath,
            Stream destinationStream,
            CancellationToken cancellationToken = default,
            IProgress<SizeChangeStatus> progress = null);
    }
}
