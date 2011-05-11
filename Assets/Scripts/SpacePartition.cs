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
		
		if (targets.Count != 0) {
			Vector3 pos = e.getObj().transform.position;
			
			SpaceEntity closestTarget = (SpaceEntity)targets[0];
			float closestSqrDist = (closestTarget.getObj().transform.position - pos).sqrMagnitude;
			
			for (int i = 1; i < targets.Count; ++i) {
				SpaceEntity target = (SpaceEntity)targets[i];
				float sqrDist = (target.getObj().transform.position - pos).sqrMagnitude;
				if (sqrDist < closestSqrDist) {
					closestTarget = target;
					closestSqrDist = sqrDist;
				}	
			}
			return closestTarget;
		}
		return null;
	}
	
}
