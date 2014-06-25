namespace Konami
{
	public interface IKonamiRepository
	{
		KonamiCode GetCode(string couponType, string first01);
	}
}