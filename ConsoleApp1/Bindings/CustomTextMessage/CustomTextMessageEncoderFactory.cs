using System.ServiceModel.Channels;

namespace ConsoleApp1.Bindings.CustomTextMessage
{
    public class CustomTextMessageEncoderFactory : MessageEncoderFactory
    {
        private readonly MessageEncoder _encoder;
        private readonly MessageVersion _version;

        public override MessageEncoder Encoder => _encoder;

        public override MessageVersion MessageVersion => _version;

        internal string MediaType { get; }

        internal string CharSet { get; }

        internal CustomTextMessageEncoderFactory(string mediaType, string charSet, MessageVersion version)
        {
            _version = version;
            MediaType = mediaType;
            CharSet = charSet;

            _encoder = new CustomTextMessageEncoder(this);
        }
    }
}
