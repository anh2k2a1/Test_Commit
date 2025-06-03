export type GoalType =
  | 'Target'
  | 'Milestone'

export interface Goal {
  id: number;
  title: string;
  type: GoalType;
  progress: number;
  target: number;
  milestoneLabels: string[];
  deadline: Date;
  isCompleted: boolean;
  name: string;           
  startAmount: number;    
  targetAmount: number;   
  milestones: number[];  

}
