using System;
using System.Collections.Generic;
using System.Text;

namespace HyperVBackup.Delivery.Adapters.YandexCloud
{
    internal class Settings
    {
        private static Settings _instance;

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public static Settings Instance => _instance ?? (_instance = new Settings());
    }
}
