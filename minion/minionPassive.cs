using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class minionPassive {

	//public minionData minion;
	public string passiveName,passiveOwner,owner;
	public int spellDamage,healing,minionIndex=0;

	public virtual void startOfTurn(bool youTurn){}
	public virtual void endOfTurn(bool youTurn){}
	public virtual void onDeath(){}
	public virtual void playTrigger(cardData cData, string ownerTag){} // when ever a card is play
	public virtual void summonTrigger(minionData mData){} // when ever a unit is summon
	public virtual void playMinionTrigger(minionData mData){} // when ever a unit is play
	public virtual void deathTrigger(minionData mData){} // when ever a unit dies
	public virtual void healTrigger(int heal, minionData mData){} // whem ever a something is heal is heal
	public virtual void healTrigger(int heal, Champion champ){}
	//public virtual void spellTrigger(spellData SD){} // when ever a spell is played
	public virtual void Update(string owner, int index){ this.owner=owner; minionIndex=index;}
	public virtual void attacking(minionData mData,minionData enemy){} //when ever unit is attacking
	public virtual void attacking(minionData mData, Champion enemy){} //when ever unit is attacking
	public virtual void damageTrigger(int damage, minionData mData){} // when ever a unit takes damage
	public virtual void drawTrigger(bool youDraw){}// when ever someone draw a card
	public virtual void discardTrigger(cardData cData){}// when ever someone draw a card
	public virtual int costBuff(Card cData){return 0;} //effect the cost of cards in either players hand.
	public virtual int atkBuff(minionData mData){return 0;}
	public virtual int hpBuff(minionData mData){return 0;}
}
