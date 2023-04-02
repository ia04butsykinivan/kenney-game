using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float _jumpForce = 400f;
	[Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
	[SerializeField] private bool _airControl = false;
	[SerializeField] private LayerMask _whatIsGround;
	[SerializeField] private Transform _groundCheck;

	const float GroundedRadius = .2f;
	private bool _grounded;
	private Rigidbody2D _rigidbody2D;
	private bool _facingRight = true;
	private Vector3 _velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	
	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = _grounded;
		_grounded = false;
		
		Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, GroundedRadius, _whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				_grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool jump)
	{
		if (_grounded || _airControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2D.velocity.y);
			_rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

			if (move > 0 && !_facingRight)
			{
				Flip();
			}
			else if (move < 0 && _facingRight)
			{
				Flip();
			}
		}
		if (_grounded && jump)
		{
			_grounded = false;
			_rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
		}
	}


	private void Flip()
	{
		_facingRight = !_facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}