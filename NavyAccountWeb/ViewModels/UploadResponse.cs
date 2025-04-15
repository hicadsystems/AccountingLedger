using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.ViewModels
{
    public class UploadResponse
    {
            public List<string> Success { get; set; } = new List<string>();
        public List<string> Errors { get; set; } = new List<string>();

    }
}
