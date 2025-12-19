import { Component, inject } from '@angular/core';
import { PlayerCard } from '../player-card/player-card';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { PlayerCardContent } from "../player-card-content/player-card-content";

@Component({
  selector: 'app-transfer-list',
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatSelectModule, MatInputModule, PlayerCard, PlayerCardContent],
  templateUrl: './transfer-list.html',
  styleUrl: './transfer-list.scss',
})
export class TransferList {
  private fb = inject(FormBuilder);

  form = this.fb.group({
    playerName: [''],
    status: [[] as number[]],
    movement: [[] as number[]],
    type: [[] as number[]],
  });
}
