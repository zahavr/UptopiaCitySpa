namespace Core.Specification.User
{
	public class UserFriendSpecParams : BaseSpecParams
    {
		private string _userEmail;

		public string UserEmail
		{
			get { return _userEmail; }
			set { _userEmail = value; }
		}

		private string _search;
		public string Search
		{
			get { return _search; }
			set { _search = value; }
		}

	}
}
