﻿#if FRAMEWORK

using System.Net;

namespace SpeakEasy
{
    public partial class RequestRunner
    {
        partial void BuildWebRequestFrameworkSpecific(HttpWebRequest webRequest)
        {
            ServicePointManager.Expect100Continue = false;
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;
        }
    }
}

#endif
