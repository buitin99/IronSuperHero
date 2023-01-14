using System;
using System.Collections.Generic;
using UnityEngine;
using MyCustomAttribute;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    /**
     * Sử dụng Player để tham chiếu đển nhân vật của player
     * Event OnSelectedPlayer để lắng nghe hành động chọn player dữ liệu đươc gửi đi là Transform player
     * enemiesCount trả về số lượng enemies còn lại
     * Event OnEnemiesDestroyed để lắng nghe khi số enemies bằng 0
     * Event OnUpdateCoin để lặng nghe khi coin đc update dữ liệu gửi đi là số lượng coin
     * OnPause, OnResume sự kiện pause và resume
     */

    [ReadOnly, SerializeField] private int amountCoins;
    [ReadOnly, SerializeField] private List<Transform> enemies = new List<Transform>();
    public int enemiesCount {
        get {
            return enemies.Count;
        }
    }
    [ReadOnly] public int levels, waves;
    public Transform player {get; private set;}
    public event Action OnEnemiesDestroyed;
    public event Action<Transform> OnSelectedPlayer;
    public event Action OnPause;
    public event Action OnResume;
    public event Action<int> OnUpdateCoin;

    //nam
    private float healthPlayer;
    public UnityEvent<float> OnUpdateHealthPlayer = new UnityEvent<float>();

    //update health player
    public void UpdatePlayHealth(float hp){
        healthPlayer = hp;
        OnUpdateHealthPlayer?.Invoke(healthPlayer);
    }

    public List<Transform> GetEnemies() {
        return enemies;
    }

    public void AddEnemy(Transform enemy) {
        enemies.Add(enemy);
    }

    //remove enemy khỏi danh sách enemies
    public void RemoveEnemy(Transform enemy) {
        enemies.Remove(enemy);
        if(enemiesCount == 0) {
            OnEnemiesDestroyed?.Invoke();
        }
    }

    //clear toàn bộ enemy
    public void ClearEnemies() {
        enemies.Clear();
    }
    
    public void PauseGame() {
        Time.timeScale = 0;
        OnPause?.Invoke();
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        OnResume?.Invoke();
    }

    //update tiền nhặt đươc;
    public void UpdateCoin(int amount) {
        amountCoins += amount;
        OnUpdateCoin?.Invoke(amountCoins);
    }

    //set tham chiếu player
    public void SelectPlayer(Transform player) {
        this.player = player;
        OnSelectedPlayer?.Invoke(this.player);
    }
    
}
