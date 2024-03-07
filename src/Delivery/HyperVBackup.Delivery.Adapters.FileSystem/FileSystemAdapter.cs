using HyperVBackup.Data.FileProgressChange;
using HyperVBackup.Delivery.Abstract;
using HyperVBackup.Delivery.Abstract.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HyperVBackup.Delivery.Adapters.FileSystem
{
    public class FileSystemAdapter : BaseDeliveryAdapter, IDeliveryAdapter
    {
        public Task Configure(IDictionary<string, string> config)
        {
            throw new NotImplementedException();
        }

        public Task Upload(
            Stream sourceStream, 
            string destinationPath, 
            bool overwrite = true, 
            CancellationToken cancellationToken = default, 
            IProgress<SizeChangeStatus> progress = null)
        {
            var fileMode = overwrite ? FileMode.Create : FileMode.CreateNew;
            using (var destinationStream = new FileStream(destinationPath, fileMode, FileAccess.Write))
            {
                return Copy(sourceStream, destinationStream, cancellationToken, progress);
            }
        }

        public Task Download(
            string sourcePath, 
            Stream destinationStream, 
            CancellationToken cancellationToken = default, 
            IProgress<SizeChangeStatus> progress = null)
        {
            using (var sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                return Copy(sourceStream, destinationStream, cancellationToken, progress);
            }
        }
    }
}
