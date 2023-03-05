using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "SO/RTD/Projectile", order = 0)]
    public class SO_Projectile : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _displayName;
        [SerializeField] private float _speed;
        [SerializeField] private bool _isRotate;

        [Header("AudioClips")]
        [SerializeField] private AudioClip _shootClip;
        [SerializeField] private AudioClip _hitClip;
        [SerializeField] private AudioClip _hitCriticalClip;

        [Header("Prefabs")]
        [SerializeField] private GameObject _pfHitFx;
        [SerializeField] private GameObject _pfProjectileModel;
        [SerializeField] private Projectile _pfProjectile;

        // Below this we will make the get properties of the fields
        public int Id => _id;
        public string DisplayName => _displayName;
        public float Speed => _speed;
        public bool IsRotate => _isRotate;
        public AudioClip ShootClip => _shootClip;
        public AudioClip HitClip => _hitClip;
        public AudioClip HitCriticalClip => _hitCriticalClip;
        public GameObject PfHitFx => _pfHitFx;
        public GameObject PfProjectileModel => _pfProjectileModel;
        public Projectile PfProjectile => _pfProjectile;
    }
}