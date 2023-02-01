using MikroDataTransferAPI.Contracts;

namespace MikroDataTransferAPI.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        public RepositoryWrapper(string connString)
        {
            this._connString = connString;
        }
        private IAuthRepository _authRepository=null;
        private readonly string _connString;

        public IAuthRepository AuthRepo
        {
            get
            {
                if (_authRepository == null)
                {
                    _authRepository = new AuthRepository(_connString);
                }

                return _authRepository;
            }
        }
        IProductRepository _productRepository=null;
        public IProductRepository ProductRepo
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_connString);
                return _productRepository;
            }
        }

        IProductRepository2 _productRepository2 = null;
        public IProductRepository2 ProductRepo2
        {
            get
            {
                if (_productRepository2 == null)
                    _productRepository2 = new ProductRepository2(_connString);
                return _productRepository2;
            }
        }


    }
}
