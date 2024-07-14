namespace futshop_dweb.Data
{
    public interface IUserService
    {
        bool IsAuthenticated { get; }

        bool IsAdmin { get; }

        bool addedToCart { get; }

        void rmCart();

        void removeCart(int id);

        bool finishedOrder { get; }

        void resetFinishedOrder();

        int getUserID();
    }
}
