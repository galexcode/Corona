using System.Collections;
using System.Collections.Generic;

public class SpacePartition {

	public ArrayList[] targets;
	
	// generics may not be supported in IOS
	public Dictionary<string, ArrayList[]> map;
	
	public int numTeams = 2;
	
	public SpacePartition() {
		targets = new ArrayList[2];
		targets[0] = new ArrayList();
		targets[1] = new ArrayList();
		
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
	
}
