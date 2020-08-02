namespace EasyAbp.EShop.Plugins.StoreApproval
{
    public static class StoreApprovalErrorCodes
    {
        public const string Namespace = "EasyAbp.EShop.Plugins.StoreApproval";

        public const string AlreadySubmitted = Namespace + ":" + "000001";
        public const string AlreadyApproved = Namespace + ":" + "000002";
        public const string NotSubmitted = Namespace + ":" + "000003";
    }
}
