
import { Component,ViewChild } from '@angular/core';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormArray,
  AbstractControl,
  ValidatorFn,
} from '@angular/forms';
import { MaterialModule } from '../../../services/material.service';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormControl } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';


export interface Goal {
  id: number;
  title: string;          // Tiêu đề mục tiêu (VD: Mua xe)
  name: string;           // Tên mục tiêu (VD: Mục tiêu mua ô tô 2025)
  type: string;           // Loại mục tiêu (investment, saving, house, car)
  startAmount: number;    // Số tiền bắt đầu
  targetAmount: number;   // Số tiền kết thúc
  milestones: number[];   // Các mốc tiền
  progress: number;       // Tiến độ (index của mốc hiện tại)
  isCompleted: boolean;   // Đã hoàn thành hay chưa  
}



@Component({
  selector: 'app-goal',
    templateUrl: './goal.component.html',
    styleUrls: ['./goal.component.css'],
  standalone: true,
  imports: [CommonModule, MatTooltipModule, MaterialModule, ReactiveFormsModule],
  animations: [
    trigger('goalStop', [
      state('inactive', style({ transform: 'scale(1)', backgroundColor: '#4caf50' })),
      state('active', style({ transform: 'scale(1.2)', backgroundColor: '#2196f3' })),
      transition('inactive <=> active', [animate('300ms ease-in-out')]),
    ]),
  ],
})
export class GoalComponent {

 

  goalTypes = [
    { value: 'Target', label: '💼 Target ' },
    { value: 'Milestone', label: '💰 Milestone' },
    { value: 'house', label: '🏠 Nhà' },
    { value: 'car', label: '🚗 Xe' }
  ];

  goals: Goal[] = [
    {
      id: 1,
      title: 'Mua xe ô tô',
      name: 'Mục tiêu mua ô tô 2025',
      type: 'car',
      startAmount: 0,
      targetAmount: 500000000,
      milestones: [50000000, 150000000, 300000000, 500000000],
      progress: 1,
      isCompleted: false,
    },
    {
      id: 2,
      title: 'Du lịch Châu Âu',
      name: 'Tiết kiệm du lịch 2024',
      type: 'saving',
      startAmount: 10000000,
      targetAmount: 100000000,
      milestones: [30000000, 60000000, 100000000],
      progress: 0,
      isCompleted: false,
    },
  ];

  showForm = false;
  goalForm: FormGroup;
  editingGoalId: number | null = null;
  formError: string | null = null;
  submitted = false;
  isLoading = false;
  isSuccess = false;

  showCongratsPopup = false;
  congratsMessage = '';
  congratsGoal: Goal | null = null;

  constructor(private fb: FormBuilder) {
    this.goalForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      type: ['', Validators.required],
      startAmount: [0, [Validators.required, Validators.min(0)]],
      targetAmount: [0, [Validators.required, Validators.min(1)]],
      milestones: this.fb.array([], this.milestonesValidator()),
      startDate: ['', Validators.required],
      endDate: ['', Validators.required]
    });

    

    // Thêm 4 mốc mặc định
    // this.addMilestone();
    // this.addMilestone();
    // this.addMilestone();
  }

  private createMilestoneControl() {
    return this.fb.control(0, [Validators.required, Validators.min(1)]);
  }

  get milestones(): FormArray {
    return this.goalForm.get('milestones') as FormArray;
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    this.formError = null;
    this.submitted = false;
    this.isSuccess = false;

    if (!this.showForm) {
      this.resetForm();
    } else {
      setTimeout(() => {
        document.querySelector('.goal-form-card')?.scrollIntoView({ behavior: 'smooth', block: 'start' });
      }, 200);
    }
  }

  
 

  addMilestone(): void {
    this.milestones.push(this.createMilestoneControl());
  }

  removeMilestone(index: number): void {
    if (this.milestones.length > 1) {
      this.milestones.removeAt(index);
    }
  }

  async addGoal(): Promise<void> {
    this.submitted = true;
    this.formError = null;
    this.goalForm.markAllAsTouched();

    if (this.goalForm.invalid) {
      return; 
    }

    this.isLoading = true;

    // Giả lập API call
    await new Promise(resolve => setTimeout(resolve, 1000));

    const isEditing = this.editingGoalId !== null;
    const newGoal: Goal = {
      id: isEditing ? this.editingGoalId! : this.getNextGoalId(),
      title: this.goalForm.value.title.trim(),
      name: this.goalForm.value.name.trim(),
      type: this.goalForm.value.type,
      startAmount: this.goalForm.value.startAmount,
      targetAmount: this.goalForm.value.targetAmount,
      milestones: this.goalForm.value.milestones,
      progress: isEditing
        ? this.goals.find((g) => g.id === this.editingGoalId!)?.progress || 0
        : 0,
      isCompleted: false,
    };

    if (isEditing) {
      const index = this.goals.findIndex((g) => g.id === this.editingGoalId);
      this.goals[index] = newGoal;
    } else {
      this.goals.push(newGoal);
    }

    this.isLoading = false;
    this.isSuccess = true;
    
    setTimeout(() => {
      this.resetForm();
      this.showForm = false;
      this.submitted = false;
      this.isSuccess = false;
    }, 1500);
  }

  editGoal(goal: Goal): void {
    this.editingGoalId = goal.id;
    this.showForm = true;
    this.formError = null;
    this.submitted = false;
    this.isSuccess = false;

    this.goalForm.patchValue({ 
      title: goal.title,
      name: goal.name,
      type: goal.type,
      startAmount: goal.startAmount,
      targetAmount: goal.targetAmount
    });

    this.milestones.clear();
    goal.milestones.forEach(amount => {
      this.milestones.push(this.fb.control(amount, [Validators.required, Validators.min(1)]));
    });

    setTimeout(() => {
      document.querySelector('.goal-form-card')?.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }, 200);
  }

  resetForm(): void {
    this.goalForm.reset({
      startAmount: 0,
      targetAmount: 0
    });
    this.milestones.clear();
    this.addMilestone();
    this.addMilestone();
    this.addMilestone();
    this.addMilestone();
    this.editingGoalId = null;
    this.submitted = false;
  }

  milestonesValidator(): ValidatorFn {
    return (control: AbstractControl) => {
      if (!(control instanceof FormArray)) {
        return null;
      }
      
      const milestones = control.value as number[];
      const targetAmount = this.goalForm?.get('targetAmount')?.value || 0;
      
      // Kiểm tra nếu có mốc nào vượt quá targetAmount
      const exceedsTarget = milestones.some(m => m > targetAmount);
      if (exceedsTarget) {
        return { exceedsTarget: true };
      }
      
      // Kiểm tra nếu các mốc không theo thứ tự tăng dần
      for (let i = 1; i < milestones.length; i++) {
        if (milestones[i] <= milestones[i-1]) {
          return { invalidOrder: true };
        }
      }
      
      return null;
    };
  }

  getTotalMilestones(): number {
    return this.milestones.value.reduce((sum: number, current: number) => sum + current, 0);
  }

  getProgressPercentage(goal: Goal): number {
    if (goal.targetAmount <= goal.startAmount) return 100;
    return Math.min(100, Math.round(((goal.milestones[goal.progress] || goal.startAmount) - goal.startAmount) / (goal.targetAmount - goal.startAmount) * 100));
  }

  getMilestoneState(goal: Goal, milestoneIndex: number): string {
    return goal.progress > milestoneIndex ? 'active' : 'inactive';
  }

  updateProgress(goal: Goal): void {
    if (goal.progress < goal.milestones.length) {
      goal.progress++;
      if (goal.progress === goal.milestones.length) {
        goal.isCompleted = true;
        this.showCongrats(goal);
      }
    }
  }

  showCongrats(goal: Goal): void {
    this.congratsGoal = goal;
    this.congratsMessage = `🎉 Chúc mừng bạn đã hoàn thành mục tiêu "${goal.title}"! 🎉`;
    this.showCongratsPopup = true;

    setTimeout(() => {
      this.closeCongratsPopup();
    }, 5000);
  }

  closeCongratsPopup(): void {
    this.showCongratsPopup = false;
    this.congratsMessage = '';
    this.congratsGoal = null;
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value);
  }

  private getNextGoalId(): number {
    return this.goals.length ? Math.max(...this.goals.map(g => g.id)) + 1 : 1;
  }
}