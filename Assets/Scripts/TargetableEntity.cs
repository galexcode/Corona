using UnityEngine;
using System.Collections;

public class TargetableEntity : SpaceEntity {

	public int team;
	
	public int shields = 0;
	public int armor;
	
	public bool alive = true;
	
	public int targeters = 0;
	
	public void Target() {
		++targeters;
	}
	
	public void Untarget() {
		--targeters;	
	}
	
	public bool CanDestroy() {
		return targeters == 0;	
	}
	
	public void TakeHit(int dmg) {
		this.armor -= dmg;
		if (this.armor <= 0) {
			this.alive = false; 	
		}
	}
	
	public bool Alive() {
		return alive;	
	}

}
