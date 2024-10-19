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

        public const string ResetPasswordEmailMessage = @"
        <h2>Password Reset Request</h2>
        <p>Hello,</p>
        <p>We received a request to reset the password for your account. If you requested this change, you can reset your password by 
        <a href='{0}' style='color: #007bff; text-decoration: none;'>clicking here</a>.</p>
        <p>If you did not request a password reset, please ignore this email. Your current password will remain unchanged.</p>
        <p>For your security, this link will expire in 24 hours. If the link has expired, you can request a new one by visiting our 
        <a href='https://arsenalfanpage.azurewebsites.net/Identity/Account/ForgotPassword' style='color: #007bff; text-decoration: none;'>Password Reset page</a>.</p>
        <br>
        <p>Thank you,</p>
        <p>The Arsenal Fan Page Team</p>";
    }
}
