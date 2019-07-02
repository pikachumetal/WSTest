namespace WsAncertConnection.NetFramework.Bindings.CustomTextMessage
{
    internal class ConfigurationStrings
    {
        public const string MessageVersion = "messageVersion";
        public const string MediaType = "mediaType";
        public const string Encoding = "encoding";
        public const string ReaderQuotas = "readerQuotas";

        public const string None = "None";
        public const string Default = "Default";
        public const string Soap11 = "Soap11";
        public const string Soap11WSAddressing10 = "Soap11WSAddressing10";
        public const string Soap11WSAddressingAugust2004 = "Soap11WSAddressingAugust2004";
        public const string Soap12 = "Soap12";
        public const string Soap12WSAddressing10 = "Soap12WSAddressing10";
        public const string Soap12WSAddressingAugust2004 = "Soap12WSAddressingAugust2004";

        public const string DefaultMessageVersion = Soap11;
        public const string DefaultMediaType = "text/xml";
        public const string DefaultEncoding = "utf-8";
    }
}
