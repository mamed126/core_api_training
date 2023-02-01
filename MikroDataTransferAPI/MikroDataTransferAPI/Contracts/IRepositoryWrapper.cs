namespace MikroDataTransferAPI.Contracts
{
    public interface IRepositoryWrapper
    {
        IAuthRepository AuthRepo { get; }
        IProductRepository ProductRepo { get; }

        IProductRepository2 ProductRepo2 { get; }
    }
}
