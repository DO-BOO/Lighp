using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMarble : ElementMarble
{
    public RedMarble(ElementMarble marble) : base(marble)
    {
        MarbleType = MarbleType.Red;
    }

    protected override void ExecuteDoubleSynergy(CharacterHp characterHp)
    {

    }

    protected override void ExecuteTripleSynergy(CharacterHp characterHp)
    {
        characterHp.DOTDeal(5f, 3); // 5초간 3 도트 대미지
    }
}
