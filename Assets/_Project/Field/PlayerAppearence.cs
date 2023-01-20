using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmericanFootballManager
{
  public enum SkinColor { brown, white };
  public enum ArmourColor { blue, red };
  public class PlayerAppearence : MonoBehaviour
  {
    Dictionary<SkinColor, Color> SkinColors = new(){
        {SkinColor.white, new Color(1.0f, 0.7950f, 0.5707f, 1.0f)},
        {SkinColor.brown, new Color(0.7075f, 0.4925f, 0.3437f, 1.0f)},
    };
    Dictionary<ArmourColor, Color> ArmourColors = new(){
        {ArmourColor.blue, Color.blue},
        {ArmourColor.red, Color.red},
    };
    public SkinColor SkinColor;
    public ArmourColor ArmourColor;
    public bool HasHair;
    public bool HasTopKnot;
    public GameObject BodyObject;
    public GameObject FaceObject;
    public GameObject FeetObject;
    public GameObject ChestObject;
    public GameObject ShoulderObject;
    public GameObject GlovesObject;
    public GameObject LegsObject;
    public GameObject HairObject;
    public GameObject TopKnotObject;
    void Start()
    {
      UpdateSkinColor(BodyObject);
      UpdateSkinColor(FaceObject);
      UpdateSkinColor(FeetObject);
      UpdateArmourColor(ChestObject);
      UpdateArmourColor(ShoulderObject);
      UpdateArmourColor(GlovesObject);
      UpdateArmourColor(LegsObject);
      if (HasHair) HairObject.SetActive(true);
      if (HasTopKnot) TopKnotObject.SetActive(true);
    }
    void UpdateSkinColor(GameObject gameObject)
    {
      Renderer renderer = gameObject.GetComponent<Renderer>();
      Material material = renderer.material;
      material.color = SkinColors[SkinColor];
    }
    void UpdateArmourColor(GameObject gameObject)
    {
      Renderer renderer = gameObject.GetComponent<Renderer>();
      Material material = renderer.material;
      material.color = ArmourColors[ArmourColor];
    }
  }
}
