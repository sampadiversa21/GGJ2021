using Platformer.Core;
using Platformer.Model;
using DG.Tweening;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public CanvasGroup pauseCG;

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public bool isPlaying = false;
        public bool isPaused = false;
        public bool cinematic1 = true;
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();

            if(!cinematic1 && Input.GetButtonDown("Menu"))
            {
                OnPausePressed();
            }
        }

        private void OnPausePressed()
        {
            isPaused = !isPaused;

            if(isPaused)
            {
                Time.timeScale = 0;

                pauseCG.DOFade(1, 0.5f).SetUpdate(true).OnComplete(() =>
                {
                    pauseCG.blocksRaycasts = true;
                });
            }
            else
            {
                Time.timeScale = 1;
                pauseCG.blocksRaycasts = false;

                pauseCG.DOFade(0, 0.5f);
            }
        }

        public void StopCinematic1()
        {
            cinematic1 = false;
        }
    }
}