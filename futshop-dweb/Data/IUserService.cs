namespace futshop_dweb.Data
{
    public interface IUserService
    {
        bool IsAuthenticated { get; }

        bool IsAdmin { get; }

        bool addedToCart { get; }

        void rmCart();
    }
}
