import { Component } from '@angular/core';
import { TransferList } from './transfer-list/transfer-list';

@Component({
  selector: 'app-player-list',
  imports: [TransferList],
  templateUrl: './player-list.html',
  styleUrl: './player-list.scss',
})
export class PlayerList {

}
