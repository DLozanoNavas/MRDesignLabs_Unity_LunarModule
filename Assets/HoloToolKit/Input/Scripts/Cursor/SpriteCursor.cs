﻿using System;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    /// <summary>
    /// Object that represents a cursor comprised of sprites and colors for each state
    /// </summary>
    public class SpriteCursor : Cursor
    {
        [Serializable]
        public struct SpriteCursorDatum
        {
            public string Name;
            public CursorStateEnum CursorState;
            public Sprite CursorSprite;
            public Color CursorColor;
        }

        [SerializeField]
        public SpriteCursorDatum[] CursorStateData;

        /// <summary>
        /// Sprite renderer to change.  If null find one in children
        /// </summary>
        public SpriteRenderer TargetRenderer;

        /// <summary>
        /// On enable look for a sprite renderer on children
        /// </summary>
        protected override void OnEnable()
        {
            if(TargetRenderer == null)
            {
                TargetRenderer = GetComponentInChildren<SpriteRenderer>();
            }

            base.OnEnable();
        }

        /// <summary>
        /// Override OnCursorState change to set the correct sprite
        /// state for the cursor
        /// </summary>
        /// <param name="state"></param>
        public override void OnCursorStateChange(CursorStateEnum state)
        {
            base.OnCursorStateChange(state);

            if (state != CursorStateEnum.Contextual)
            {
                for (int i = 0; i < CursorStateData.Length; i++)
                {
                    if (CursorStateData[i].CursorState == state)
                    {
                        SetCursorState(CursorStateData[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Based on the type of state info pass it through to the sprite renderer
        /// </summary>
        /// <param name="stateDatum"></param>
        private void SetCursorState(SpriteCursorDatum stateDatum)
        {
            // Return if we do not have an animator
            if (TargetRenderer != null)
            {
                TargetRenderer.sprite = stateDatum.CursorSprite;
                TargetRenderer.color = stateDatum.CursorColor;
            }
        }

    }

}