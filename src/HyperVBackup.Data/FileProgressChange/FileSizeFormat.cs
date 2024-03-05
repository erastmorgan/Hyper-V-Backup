using System;
using System.Collections.Generic;
using System.Text;

namespace HyperVBackup.Data.FileProgressChange
{
    public class FileSizeFormat
    {
        public FileSizeFormat(decimal value, StorageUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Gets or sets the value of file size in specified <see cref="Unit"/>.
        /// </summary>
        public decimal Value { get; internal set; }

        /// <summary>
        /// Gets or sets the measure unit of data.
        /// </summary>
        public StorageUnit Unit { get; internal set; }
    }
}
