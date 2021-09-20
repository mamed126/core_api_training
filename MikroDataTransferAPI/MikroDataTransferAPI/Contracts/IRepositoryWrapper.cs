namespace MikroDataTransferAPI.Contracts
{
    public interface IRepositoryWrapper
    {
        IAuthRepository AuthRepo { get; }
        IProductRepository ProductRepo { get; }
    }
}
