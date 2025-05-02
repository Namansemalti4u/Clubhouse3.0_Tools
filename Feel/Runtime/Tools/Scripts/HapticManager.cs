using System.Collections;
using UnityEngine;
using Lofelt.NiceVibrations;
using Clubhouse.Helper;

public enum HapticType
{
    OnButtonClick,
    OnCorrect,
    OnWrong,
    OnGameStart,
    OnTimerEnd
}

public class HapticManager : Singleton<HapticManager>
{
    [Header("Haptic Settings")]
    private bool enableHaptics = true;
    private Coroutine continuousHapticCoroutine;
    
    override protected void Awake()
    {
        base.Awake();  
        
        // Initialize haptics
        // enableHaptics = AppData.EnableHaptic;
        HapticController.hapticsEnabled = enableHaptics;
    }

    void Update()
    {
        
    }

    public void PlayHapticOnButtonPressed()
    {
        if (!enableHaptics) return;
        
        // Play a impact feedback
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
//        Debug.Log("Haptics feedback played");
    }

    public void PlayHaptic(HapticType a_type)
    {
        PlayHaptic(a_type switch{
            HapticType.OnButtonClick => HapticPatterns.PresetType.LightImpact,
            HapticType.OnCorrect => HapticPatterns.PresetType.LightImpact,
            HapticType.OnWrong => HapticPatterns.PresetType.SoftImpact,
            HapticType.OnGameStart => HapticPatterns.PresetType.Success,
            HapticType.OnTimerEnd => HapticPatterns.PresetType.Success,
            _ => HapticPatterns.PresetType.None,
        });
    }

    // Public method to play different types of haptic feedback
    public void PlayHaptic(HapticPatterns.PresetType presetType)
    {
        if (!enableHaptics) return;
        HapticPatterns.PlayPreset(presetType);
    }

    public void SetHapticsEnabled(bool enabled)
    {
        enableHaptics = enabled;
        HapticController.hapticsEnabled = enabled;
    }

    // Play a single custom haptic with specified intensity and sharpness
    public void PlayCustomHaptic(float intensity = 1.0f, float sharpness = 0.5f, float duration = 0.1f)
    {
        if (!enableHaptics) return;
        HapticPatterns.PlayConstant(intensity, sharpness, duration);
    }

    // Start continuous haptic feedback with specified intensity and sharpness
    public void StartContinuousHaptic(float intensity = 1.0f, float sharpness = 0.5f)
    {
        if (!enableHaptics || continuousHapticCoroutine != null) return;
        
        continuousHapticCoroutine = StartCoroutine(ContinuousHapticRoutine(intensity, sharpness));
    }

    // Stop continuous haptic feedback
    public void StopContinuousHaptic()
    {
        if (continuousHapticCoroutine != null)
        {
            StopCoroutine(continuousHapticCoroutine);
            continuousHapticCoroutine = null;
            HapticController.Stop();
        }
    }

    private IEnumerator ContinuousHapticRoutine(float intensity, float sharpness)
    {
        while (true)
        {
            PlayCustomHaptic(intensity, sharpness);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDisable()
    {
        StopContinuousHaptic();
    }
}
