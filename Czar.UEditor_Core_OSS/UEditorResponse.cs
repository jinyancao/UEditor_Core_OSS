﻿namespace Czar.UEditor_Core_OSS
{
    public class UEditorResponse
    {
       
        public UEditorResponse(string contentType, string result)
        {
            ContentType = contentType;
            Result = result;
        }

        public string ContentType { get; set; }

        public string Result { get; set; }
    }
}
