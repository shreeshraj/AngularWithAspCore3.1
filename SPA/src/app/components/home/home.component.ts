import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
 Isactive:boolean=false;
  public toggleRegOption(){
    this.Isactive=!this.Isactive;
  }

}
