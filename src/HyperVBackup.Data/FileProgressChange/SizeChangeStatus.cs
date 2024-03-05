using System;
using System.Collections.Generic;
using System.Text;

namespace HyperVBackup.Data.FileProgressChange
{
    public class SizeChangeStatus
    {
        public SizeChangeStatus(FileSize total, FileSize current)
        {
            Total = total;
            Current = current;
            Percentage = (int)(current.BytesValue * 100 / total.BytesValue);
        }

        /// <summary>
        /// Gets the total size of data.
        /// </summary>
        public FileSize Total { get; }

        /// <summary>
        /// Gets the current processed size of data.
        /// </summary>
        public FileSize Current { get; }

        /// <summary>
        /// Gets the percentage of <see cref="Current"/> and <see cref="Total"/> size.
        /// </summary>
        public int Percentage { get; }
    }
}
