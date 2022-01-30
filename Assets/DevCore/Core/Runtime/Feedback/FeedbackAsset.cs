using UnityEngine.Serialization;

namespace DevCore.Core {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "FB_", menuName = Constants.ASSET_PATH + "Feedback", order = 0)]
    public class FeedbackAsset : ScriptableObject {
        #region Constants
        private const int BasePoolSize = 40;
        #endregion

        #region Datas
        [Header("Visual")]
        [SerializeField] private ParticleSystem particleSystem = null;
        [SerializeField] private bool relativeRotation = false;
        [SerializeField] private bool relativeScale = false;

        [FormerlySerializedAs("soundEffect")]
        [Header("Sound")]
        [SerializeField] private AudioClip[] soundEffects = {};
        [SerializeField] private Vector2 pitchRange = Vector2.one;
        [SerializeField, Min(0f)] private Vector2 volumeRange = new Vector2(0.8f, 1f);
        [Tooltip("0 = 2D -> 1 = 3D")]
        [SerializeField, Range(0f, 1f)] private float spatialBlend = 1f;
        [SerializeField] private bool resetSound = false;
        #endregion

        #region Currents
        private static Feedback feedbackPrefab = null;
        internal List<Feedback> pool = new List<Feedback>(BasePoolSize);
        private Feedback lastInstance = null;
        #endregion

        #region Properties
        public Vector2 PitchRange => pitchRange;
        public AudioClip[] SoundEffects => soundEffects;
        public ParticleSystem ParticleSystem => particleSystem;
        public Vector2 VolumeRange => volumeRange;
        public float SpatialBlend => spatialBlend;

        #endregion

        #region Init
        [RuntimeInitializeOnLoadMethod]
        private static void OnInit() {
            feedbackPrefab = (Resources.Load("DevCore/Feedback") as GameObject).GetComponent<Feedback>();
        }
        #endregion

        #region Methods
        public void Play(Transform targetTransform) {
            Quaternion rotation = relativeRotation ? targetTransform.rotation : Quaternion.identity;
            Feedback inst;

            inst = GetInstance(targetTransform.position, rotation);

            if (relativeScale) {
                inst.transform.localScale = targetTransform.localScale;
            } else {
                inst.transform.localScale = Vector3.one;
            }

            if (resetSound && lastInstance != null && lastInstance != this) {
                lastInstance.audioSource.Stop();
            }

            lastInstance = inst;

            inst.Play();
        }

        private Feedback GetInstance(Vector3 position, Quaternion rotation) {
            Feedback CreateInstance() {
                Feedback inst = Instantiate(feedbackPrefab, position, rotation);
                inst.Setup(this);
                return inst;
            }
            if(pool.Count > 0) {
                if(pool[pool.Count - 1] == null) {
                    pool = new List<Feedback>(BasePoolSize);
                    return CreateInstance();
                } else {
                    Feedback fb = pool[pool.Count - 1];
                    pool.RemoveAt(pool.Count - 1);
                    fb.transform.position = position;
                    fb.transform.rotation = rotation;
                    return fb;
                }
            } else {
                return CreateInstance();
            }
        }
        #endregion
    }
}