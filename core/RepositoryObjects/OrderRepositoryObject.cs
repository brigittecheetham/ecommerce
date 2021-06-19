namespace core.RepositoryObjects
{
    public class OrderRepositoryObject : IRepositoryParameters
    {
        public string BuyerEmail { get; set; }
        public string PaymentIntentId { get; set; }
    }
}