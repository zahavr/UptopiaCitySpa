import {Component, OnInit} from '@angular/core';
import {AccountService} from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Utopia City';
  constructor(private accountService: AccountService) {
  }
  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser(): void {
    this.accountService.loadCurrentUser().subscribe(() => {
    }, error => {
      console.log(error);
    });
  }
}
