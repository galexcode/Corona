using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePartition {
	
	// generics may not be supported in IOS
	public Dictionary<string, ArrayList[]> map;
	
	public int numTeams = 2;
	
	public SpacePartition() {
		map = new Dictionary<string, ArrayList[]>();
		map.Add("targets", prepArrays());
		map.Add("lasers", prepArrays());
	}
	
	private ArrayList[] prepArrays() {
		ArrayList[] teams = new ArrayList[numTeams];
		for (int i = 0; i < numTeams; ++i) {
			teams[i] = new ArrayList();	
		}
		return teams;
	}
	
	// currently checks all targets
	public SpaceEntity NearestEnemy(SpaceEntity e) {
		
		ArrayList targets = map["targets"][(e.team + 1) % 2];
		
		if (targets.Count != 0 && ((TargetableEntity)targets[0]).Alive()) {
			Vector3 pos = e.getObj().transform.position;
			
			SpaceEntity closestTarget = (SpaceEntity)targets[0];
			float closestSqrDist = (closestTarget.getObj().transform.position - pos).sqrMagnitude;
			
			for (int i = 1; i < targets.Count; ++i) {
				TargetableEntity target = (TargetableEntity)targets[i];
				float sqrDist = (target.getObj().transform.position - pos).sqrMagnitude;
				if (sqrDist < closestSqrDist && target.Alive()) {
					closestTarget = target;
					closestSqrDist = sqrDist;
				}	
			}
			return closestTarget;
		}
		return null;
	}
	
	public RaycastHit[] ObjectsInRange(Vector3 pos, float range) {
		return Physics.SphereCastAll(pos, range, Vector3.one, 1);
	}
	
	public TargetableEntity NearestEnemyInRange(SpaceEntity e, float range) {
		Vector3 pos = e.getObj().transform.position;
		RaycastHit[] rha = this.ObjectsInRange(pos, range);
		
		TargetableEntity closestTarget = null;
		float closestSqrDist = Mathf.Infinity;
		foreach (RaycastHit rh in rha) {
			TargetableEntity poss = rh.collider.gameObject.GetComponent<TargetableEntity>();
			if (poss != null && poss.team != e.team) {
				float sqrDist = (rh.transform.position - pos).sqrMagnitude;
				if (sqrDist < closestSqrDist && poss.Alive()) {
					closestTarget = poss;
					closestSqrDist = sqrDist;
				}
			}
		}
		//Debug.Log(closestTarget);
		return closestTarget;
	}
	
}
