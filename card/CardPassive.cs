using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class CardPassive {

	public string passName,cardType="";


	//public bool inHand =false;
	public string postion="deck";
	//public Card card;
	public cardData cData;

	public virtual void startOfTurn(bool youTurn){}
	public virtual void endOfTurn(bool youTurn){}
	public virtual void onDeath(){}
	public virtual void playTrigger(cardData cData){} // when ever a card is play
	public virtual void summonTrigger(string race, bool isFriendly){} // when ever a unit is summon
	public virtual void deathTrigger(minionData mData){} // when ever a unit dies
	public virtual void healTrigger(int heal, minionData mData){} // whem ever a something is heal
	public virtual void healTrigger(int heal, Champion champ){} // whem ever a something is heal
	//public virtual void spellTrigger(spellData SD){} // when ever a spell is played

	public virtual void Update(cardData data){cData=data;}

	public virtual void attacking(minionData mData,minionData enemy){} //when ever unit is attacking
	public virtual void attacking(minionData mData, Champion enemy){} //when ever unit is attacking
	public virtual void damageTrigger(int damage, minionData mData){} // when ever a unit takes damage
	public virtual void drawTrigger(bool youDraw){}// when ever someone draw a card
	public virtual void discardTrigger(cardData cData){}// when ever someone draw a card
	public virtual void discarded(){}//when this card is discard.
	public virtual int costBuff(){ return 0;}
}
