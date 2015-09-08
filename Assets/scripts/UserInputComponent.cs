
using UnityEngine;

public class UserInputComponent : MonoBehaviour {
	public const string HorizAxisName = "Horizontal";
	public const string VertAxisName = "Vertical";

	public enum KeyIndices {
		SPACE,

		MAX
	}

	private float mAxisX;
	private float mAxisY;
	private bool[] mKeys;
	
	public float AxisX {
		get {return mAxisX;}
	}
	
	public float AxisY {
		get {return mAxisY;}
	}

	public void Reset() {
		mAxisX = 0.0f;
		mAxisY = 0.0f;

		for (int i = 0; i < (int)KeyIndices.MAX; ++i) {
			mKeys[i] = false;
		}
	}

	public void Start() {
		mKeys = new bool[(int)KeyIndices.MAX] {false};
	}
	
	public void Update() {
		mKeys[(int)KeyIndices.SPACE] = Input.GetKey(KeyCode.Space);

		mAxisX = Input.GetAxis(HorizAxisName);
		mAxisY = Input.GetAxis(VertAxisName);
	}

	public bool IsPressingJump() {
		return mKeys[(int)KeyIndices.SPACE];
	}
};

