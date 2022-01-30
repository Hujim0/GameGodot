using Godot;
using GodotGame.Dialogues;
using System;

namespace GodotGame.PlayerBehaviour
{
	public class Player : KinematicBody2D
	{
		public Vector2 input { get; private set; }
		public Vector2 velocity { get; private set; }

		public bool isRunning = false;

		bool isFacingRight = true;
		public bool IsFacingRight
		{
			get => isFacingRight;

			set
			{
				if (value == isFacingRight) return;

				FlipPlayer();
				isFacingRight = value;
			}
		}
		public static Player Instance = null;

		public delegate void OnInputUpdate(Vector2 input);
		public static OnInputUpdate InputUpdated;

		#region consts

		public const float PLAYER_SPEED = 90f;

		public const string INPUT_MOVE_UP = "ui_up";
		public const string INPUT_MOVE_LEFT = "ui_left";
		public const string INPUT_MOVE_DOWN = "ui_down";
		public const string INPUT_MOVE_RIGHT = "ui_right";

		public const string INPUT_INTERACT = "ui_select";

		public const string ANIMATION_RUN = "pl_run";
		public const string ANIMATION_IDLE = "pl_idle";

		#endregion

		Vector2 PlayerSpeed = new Vector2
		{
			x = PLAYER_SPEED,
			y = -PLAYER_SPEED,
		};


		AnimationPlayer anim = null;

		public override void _Ready()
		{
			if (Instance != null) { QueueFree(); return; }
			Instance = this;

			anim = GetNode<AnimationPlayer>("AnimationPlayer");

			DialogueSystem.OnToggled += SetPause;
			SceneManager.OnSceneInstance += ChangePosition;
			SceneManager.OnSceneInstance += _ => SetPause(false);
			SceneManager.OnSceneStartedLoading += SetPauseTrue;
		}

		private void SetPauseTrue() => SetPause(true);

        public override void _Process(float delta)
		{
			//0.375 for first step
			//0.45 for one step

			if (isRunning) anim.Play(ANIMATION_RUN);
			else anim.Play(ANIMATION_IDLE);
		}
/*			if (isRunning && !isStartedRunning)
			{
				StartAnimationRunning();
				isStartedRunning = true;
				isStartedRunning = false;
				return;
			}
			if (!isRunning && !isStoppedRunning)
			{
				StopAnimationRunning();
				isStoppedRunning = true;
				isStartedRunning = false;
			}
		}
*//*		void StartAnimationRunning()
		{
			anim.PlaybackSpeed = 1f;
			anim.Play(ANIMATION_RUN);
			timer.OneShot = true;
			timer.Start(0.375f); //first step;
		}
*//*
		void StopAnimationRunning()
		{
			anim.PlaybackSpeed = 1.25f;
		}
*//*
		void OnTimerTimeout()
		{
			if (isRunning)
			{
				if (timer.WaitTime == 0.375f) { timer.OneShot = false; timer.Start(0.45f); }

				return;	
			}


			timer.Stop();
			anim.Play(ANIMATION_IDLE);
		}

*/
		public override void _PhysicsProcess(float delta)
		{
			velocity = MoveAndSlide(input.Normalized() * PlayerSpeed);

			isRunning = velocity.Abs() > Vector2.Zero;
		}
		public override void _UnhandledInput(InputEvent @event)
		{
			if (@event.IsEcho()) return;

			input = new Vector2()
			{
				x = Input.GetActionStrength(INPUT_MOVE_RIGHT) - Input.GetActionStrength(INPUT_MOVE_LEFT),
				y = Input.GetActionStrength(INPUT_MOVE_UP) - Input.GetActionStrength(INPUT_MOVE_DOWN),
			};

			InputUpdated?.Invoke(input);

			if (input == Vector2.Zero) return;

			if ((IsFacingRight && input.x < 0) || (!IsFacingRight && input.x > 0)) IsFacingRight = !IsFacingRight;
		}

		void FlipPlayer() => ApplyScale(new Vector2(-1f, 1f));

		public static string GetNodePath()
		{
			return Instance.GetPath();
		}
		public void SetPause(bool ctx)
		{
			anim.Play(ANIMATION_IDLE);

			input = Vector2.Zero;
			SetProcessUnhandledInput(!ctx);
			SetPhysicsProcess(!ctx);
			SetProcess(!ctx);
		}
        private void ChangePosition(Vector2 pos)
        {
			if (pos == Vector2.Zero) return;
			Position = pos;
        }
	}
}
