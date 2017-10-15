namespace Veises.SocialNet.Identity.Api.V1.Models
{
	/// <summary>
	/// User unique identifier
	/// </summary>
	public struct UserUid
	{
		private long Id;

		public UserUid(long userId)
		{
			Id = userId;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (!(obj is UserUid))
				return false;

			return Id.GetHashCode() == obj.GetHashCode();
		}

		public override string ToString()
		{
			return Id.ToString();
		}
	}
}