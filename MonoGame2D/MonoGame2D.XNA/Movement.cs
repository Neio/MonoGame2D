using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    public abstract class NMovement
    {
        #region StartArg

        public Vector2 StartPosition { get; set; }

        public float BeginRadian { get; set; }

        public float BeginScal { get; set; }

        #endregion StarArg

        #region EndArgs

        public Vector2 EndPosition { get; set; }
        public float EndRadian { get; set; }
        public float EndScal { get; set; }

        #endregion EndArgs

        #region CurrentParams

        public Vector2 CurrentPosition { get; protected set; }

        public Vector2 CurrentSpeed { get; protected set; }

        public float CurrentRadian { get; protected set; }

        public float CurrentScale { get; protected set; }

        #endregion CurrentParams

        public abstract event EventHandler MovementEnded;

        public abstract Vector2 Update(float time);

        public abstract Vector2 Delta(float delta);
    }

    /// <summary>
    /// A natural movement helper (speed up then slow down)
    /// </summary>
    public class NMovement1 : NMovement
    {



        public float TopSpeedDistance { get; set; }

        protected double TopSpeed = 0;

        protected double T1 = 0;

        protected double T2 = 0;

        public double ReduceAcc = 2000f;

        protected Vector2 A1;

        protected Vector2 A2;

        protected bool isInited = false;

        protected float startTime = 0;

        protected float distance = 0;

        protected void Init()
        {
            var dis = new Vector2(EndPosition.X - StartPosition.X, EndPosition.Y - StartPosition.Y);

            distance = dis.Length();

            T2 = Math.Sqrt((distance - TopSpeedDistance) * 2 / ReduceAcc);

            TopSpeed = T2 * ReduceAcc;

            T1 = TopSpeedDistance * 2 / TopSpeed;

            dis.Normalize();

            var a1 = TopSpeed / T1;
            var norm = dis;
            norm.Normalize();
            A1 = new Vector2((float)a1 * norm.X,
                (float)a1 * norm.Y);


            A2 = new Vector2(-(float)ReduceAcc * norm.X,
                -(float)ReduceAcc * norm.Y);

            isInited = true;
        }

        public override event EventHandler MovementEnded;

        protected float currentTime = 0;

        public override Vector2 Delta(float delta)
        {
            return Update(delta + currentTime);
        }

        public override Vector2 Update(float time)
        {
            if (!isInited)
            {
                Init();
                startTime = time;
            }

            currentTime = time;

            var ctime = time - startTime;

            if (ctime < T1)
            {
                CurrentPosition = new Vector2(
                    StartPosition.X + A1.X * ctime * ctime / 2,
                    StartPosition.Y + A1.Y * ctime * ctime / 2
                    );

                CurrentSpeed = new Vector2(
                        A1.X * ctime,
                        A1.Y * ctime
                    );

                CurrentRadian = (float)(BeginRadian + (EndRadian - BeginRadian) * ctime / (T1 + T2));
                CurrentScale = BeginScal + (float)((EndScal - BeginScal) *
                    (new Vector2(CurrentPosition.X - StartPosition.X,
                        CurrentPosition.Y - StartPosition.Y).Length()
                        )
                    / distance);
            }
            else if (ctime > T1 + T2)
            {
                CurrentPosition = EndPosition;
                CurrentSpeed = new Vector2(0, 0);
                CurrentRadian = EndRadian;
                CurrentScale = EndScal;

                if (MovementEnded != null)
                {
                    MovementEnded(this, EventArgs.Empty);
                }

            }
            else
            {
                var rtime = (float)(T1 + T2 - ctime);
                CurrentPosition = new Vector2(
                    EndPosition.X + A2.X * rtime * rtime / 2,
                    EndPosition.Y + A2.Y * rtime * rtime / 2
                    );

                CurrentSpeed = new Vector2(
                        A2.X * rtime,
                        A2.Y * rtime
                    );
                CurrentRadian = (float)(BeginRadian + (EndRadian - BeginRadian) * ctime / (T1 + T2));
                CurrentScale = BeginScal + (float)((EndScal - BeginScal) *
                    (new Vector2(CurrentPosition.X - StartPosition.X,
                        CurrentPosition.Y - StartPosition.Y).Length()
                        )
                    / distance);
            }
            return CurrentPosition;
        }


    }

    /// <summary>
    /// A natural movement helper (speed up then slow down)
    /// </summary>
    public class NMovement2 : NMovement
    {



        public float TotalTime { get; set; }

        protected float T1;

        protected float T2;

        protected float TopSpeed;

        protected Vector2 A1;

        protected Vector2 A2;

        protected bool isInited = false;

        protected float startTime = 0;

        protected float distance = 0;

        protected void Init()
        {
            var dis = new Vector2(EndPosition.X - StartPosition.X, EndPosition.Y - StartPosition.Y);

            distance = dis.Length();

            TopSpeed = distance * 2 / TotalTime;


            T1 = TotalTime / 2;

            T2 = TotalTime / 2;


            dis.Normalize();

            var a1 = TopSpeed / T1;
            A1 = new Vector2((float)a1 * dis.X,
                (float)a1 * dis.Y);

            A2 = -A1;

            isInited = true;
        }

        public override event EventHandler MovementEnded;

        protected float currentTime = 0;

        public override Vector2 Delta(float delta)
        {
            return Update(delta + currentTime);
        }

        public override Vector2 Update(float time)
        {
            if (!isInited)
            {
                Init();
                startTime = time;
            }

            currentTime = time;

            var ctime = time - startTime;

            if (ctime < T1)
            {
                CurrentPosition = new Vector2(
                    StartPosition.X + A1.X * ctime * ctime / 2,
                    StartPosition.Y + A1.Y * ctime * ctime / 2
                    );

                CurrentSpeed = new Vector2(
                        A1.X * ctime,
                        A1.Y * ctime
                    );

                CurrentRadian = (float)(BeginRadian + (EndRadian - BeginRadian) * ctime / (T1 + T2));
                CurrentScale = BeginScal + (float)((EndScal - BeginScal) *
                    (new Vector2(CurrentPosition.X - StartPosition.X,
                        CurrentPosition.Y - StartPosition.Y).Length()
                        )
                    / distance);
            }
            else if (ctime > T1 + T2)
            {
                CurrentPosition = EndPosition;
                CurrentSpeed = new Vector2(0, 0);
                CurrentRadian = EndRadian;
                CurrentScale = EndScal;

                if (MovementEnded != null)
                {
                    MovementEnded(this, EventArgs.Empty);
                }

            }
            else
            {
                var rtime = (float)(T1 + T2 - ctime);
                CurrentPosition = new Vector2(
                    EndPosition.X + A2.X * rtime * rtime / 2,
                    EndPosition.Y + A2.Y * rtime * rtime / 2
                    );

                CurrentSpeed = new Vector2(
                        A2.X * rtime,
                        A2.Y * rtime
                    );
                CurrentRadian = (float)(BeginRadian + (EndRadian - BeginRadian) * ctime / (T1 + T2));
                CurrentScale = BeginScal + (float)((EndScal - BeginScal) *
                    (new Vector2(CurrentPosition.X - StartPosition.X,
                        CurrentPosition.Y - StartPosition.Y).Length()
                        )
                    / distance);
            }
            return CurrentPosition;
        }


    }
}
