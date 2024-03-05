using System;
using System.Collections.Generic;
using System.Text;

namespace HyperVBackup.Data.FileProgressChange
{
    public class FileSize
    {
        private const long BYTES_1KB = 1024;
        private const long BYTES_1MB = 1048576;
        private const long BYTES_1GB = 1073741824;
        private const long BYTES_1TB = 1099511627776;

        private decimal _default = 0;

        public FileSize(long bytesValue)
        {
            _default = bytesValue;

            if (_default < BYTES_1KB)
            {
                FormattedValue.Unit = StorageUnit.Byte;
                FormattedValue.Value = _default;
            }
            else if (_default >= BYTES_1KB && _default < BYTES_1MB)
            {
                FormattedValue.Unit = StorageUnit.Kb;
                FormattedValue.Value = _default / BYTES_1KB;
            }
            else if (_default >= BYTES_1MB && _default < BYTES_1GB)
            {
                FormattedValue.Unit = StorageUnit.Mb;
                FormattedValue.Value = _default / BYTES_1MB;
            }
            else if (_default >= BYTES_1GB && _default < BYTES_1TB)
            {
                FormattedValue.Unit = StorageUnit.Gb;
                FormattedValue.Value = _default / BYTES_1GB;
            }
            else
            {
                FormattedValue.Unit = StorageUnit.Tb;
                FormattedValue.Value = _default / BYTES_1TB;
            }
        }

        /// <summary>
        /// Gets or sets the file size in <see cref="StorageUnit.Byte"/> unit.
        /// </summary>
        public long BytesValue 
        { 
            get => (long)_default; 
        }

        /// <summary>
        /// Gets the formatted size, 
        /// where size value and his unit is automatically computed to friendly perceptibility.
        /// </summary>
        public FileSizeFormat FormattedValue { get; } = new FileSizeFormat(0, StorageUnit.Byte);
    }
}
