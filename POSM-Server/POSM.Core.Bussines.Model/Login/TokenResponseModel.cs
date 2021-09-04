namespace POSM.Core.Bussines.Model.Login
{
	public class TokenResponseModel
	{
		public string ResponseMessage { get; set; }
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
