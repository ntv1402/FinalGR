using UnityEngine;

namespace SupanthaPaul
{
	public class InputSystem : MonoBehaviour
	{
		// input string caching
		static readonly string HorizontalInput = "Horizontal";
		static readonly string JumpInput = "Jump";
		static readonly string DashInput = "Dash";
		static readonly string ShootInput = "Fire1";
		static readonly string Shootrelease = "Fire1";
		static readonly string InteractInput = "Submit";
		static readonly string DownInput = "Vertical";
        public static float HorizontalRaw()
		{
			return Input.GetAxisRaw(HorizontalInput);
		}

		public static bool Jump()
		{
			return Input.GetButtonDown(JumpInput);
		}

		public static bool JumpRelease()
		{
			return Input.GetButtonUp(JumpInput);
		}

		public static bool Dash()
		{
			return Input.GetButtonDown(DashInput);
		}

		public static bool Shoot()
		{
			return Input.GetButton(ShootInput);
		}

		public static bool shootRelease()
		{
			return Input.GetButtonUp(Shootrelease);
		}

		public static bool Interact()
		{
			return Input.GetButtonDown(InteractInput);
		}

		public static float Down()
		{
			return Input.GetAxisRaw(DownInput);
		}
	}
}
