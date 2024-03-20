namespace FCArsenalFanPage.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "FCArsenalFanPage";

        public const string AdministratorRoleName = "Administrator";

        public const string AssistantAdministrator = "Administration Rights";

        public const string ContentCreaterRoleName = "Content Creater";

        public const string MerchandisingSpecialistRoleName = "Merchandising Specialist";

        public const string UserRoleName = "User";

        public const string NewsAdministration = $"{AdministratorRoleName}, {AssistantAdministrator}, {ContentCreaterRoleName}";

        public const string MerchandisAdministration = $"{AdministratorRoleName}, {AssistantAdministrator}, {MerchandisingSpecialistRoleName}";
    }
}
