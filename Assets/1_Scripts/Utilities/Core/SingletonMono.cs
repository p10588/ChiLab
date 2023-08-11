using UnityEngine;

namespace Chi.Utilities.Core
{
	public abstract class SingletonMono<T> : MonoBehaviour where T : Component
	{
		[SerializeField] private bool DontDestroy = true;

		#region Fields

		/// <summary>
		/// The instance.
		/// </summary>
		private static T instance;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static T Instance {
			get {
				if (instance == null) {
					instance = FindObjectOfType<T>();
					if (instance == null) {
						GameObject obj = new GameObject();
						obj.name = typeof(T).Name;
						instance = obj.AddComponent<T>();
					}
				}
				return instance;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Use this for initialization.
		/// </summary>
		protected virtual void Awake() {
			if (instance == null) {
				instance = this as T;
				if(DontDestroy) DontDestroyOnLoad(gameObject);
			} else {
				Destroy(gameObject);
			}
		}

		protected abstract void Start();
		#endregion

	}
}