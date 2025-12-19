import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Header } from '../../components/header/header';
import { Footer } from '../../components/footer/footer';
import { PlayerList } from '../../components/player-list/player-list';

@Component({
  selector: 'app-home',
  imports: [CommonModule, Header, Footer, PlayerList],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {

}
