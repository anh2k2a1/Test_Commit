import { Component, Inject } from '@angular/core';
import { MaterialModule } from '../../services/material.service';
import { Course } from '../../models/course.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-transaction-dialog',
  imports: [MaterialModule],
  templateUrl: './transaction-dialog.component.html',
  styleUrl: './transaction-dialog.component.css',
})
export class TransactionDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<TransactionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Course
  ) {}

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }
}
