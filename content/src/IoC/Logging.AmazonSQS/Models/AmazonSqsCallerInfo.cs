namespace Linn.Template.IoC.Logging.AmazonSQS
{
    public class AmazonSqsCallerInfo
    {
        public string Class { get; set; }

        public string Method { get; set; }

        public string File { get; set; }

        public int Line { get; set; }
    }
}