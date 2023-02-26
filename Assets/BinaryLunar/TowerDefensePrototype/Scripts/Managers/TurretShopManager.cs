using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShopManager : MonoBehaviour
{
    public static TurretShopManager Instance;

    [SerializeField] private GameObject turretCardPrefab;
    [SerializeField] private Transform turretPanelContainer;

    [Header("Turret Settings")]
    [SerializeField] private TurretSettings[] turrets;
    List<TurretCard> _cards = new List<TurretCard>();
    public List<TurretCard> Cards => _cards;

    private Node_Old _currentNodeSelected;
    public int CurrentNodeIndex => _currentNodeSelected.Index;

    int _turretId = 0;
    public int TuuretId => _turretId;

    List<Turret> _turretList = new List<Turret>();
    public List<Turret> TurretList => _turretList;

    public Turret GetTurret(int index)
    {
        return TurretList[index];
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].turretCardIndex = i;
            CreateTurretCard(turrets[i]);
        }
    }

    private void CreateTurretCard(TurretSettings turretSettings)
    {
        GameObject newInstance = Instantiate(turretCardPrefab, turretPanelContainer.position, Quaternion.identity);
        newInstance.transform.SetParent(turretPanelContainer);
        newInstance.transform.localScale = Vector3.one;

        TurretCard cardButton = newInstance.GetComponent<TurretCard>();
        cardButton.SetupTurretButton(turretSettings);

        _cards.Add(cardButton);
    }
    
    public void NodeSelected(Node_Old nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
    }
    
    public void PlaceTurret(TurretSettings turretLoaded)
    {
        if (_currentNodeSelected != null)
        {
            GameObject turretInstance = Instantiate(turretLoaded.TurretPrefab);
            turretInstance.transform.localPosition = _currentNodeSelected.transform.position;
            turretInstance.transform.parent = _currentNodeSelected.transform;

            Turret turretPlaced = turretInstance.GetComponent<Turret>();
            turretPlaced.Id = _turretId++;
            _currentNodeSelected.SetTurret(turretPlaced);

            _turretList.Add(turretPlaced);
        }
    }

    private void TurretSold()
    {
        StartCoroutine(CODelayTurretSold());
        IEnumerator CODelayTurretSold()
        {
            yield return new WaitForEndOfFrame();
            _turretList.Remove(_currentNodeSelected.Turret);
            _currentNodeSelected = null;
        }
    }
    
    private void OnEnable()
    {
        Node_Old.OnNodeSelected += NodeSelected;
        Node_Old.OnTurretSold += TurretSold;
        TurretCard.OnPlaceTurret += PlaceTurret;
    }

    private void OnDisable()
    {
        Node_Old.OnNodeSelected -= NodeSelected;
        Node_Old.OnTurretSold -= TurretSold;
        TurretCard.OnPlaceTurret -= PlaceTurret;
    }
}
