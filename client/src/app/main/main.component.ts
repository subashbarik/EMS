import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { combineLatest, map } from 'rxjs';
import {
  loadGlobal,
  loadGlobalSuccess,
} from '../state/appglobal/appglobal.actions';
import {
  globalLoadingStatus,
  selectGlobal,
} from '../state/appglobal/appglobal.selectors';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  public global$ = this.store.select(selectGlobal);
  public globalLoadStatus$ = this.store.select(globalLoadingStatus);
  public vm$ = combineLatest([this.global$, this.globalLoadStatus$]).pipe(
    map(([global, globalLoadStatus]) => ({
      global,
      globalLoadStatus,
    }))
  );
  constructor(
    private store: Store,
    private actions: Actions,
    private router: Router
  ) {}
  ngOnInit(): void {
    console.log('main component');
    // loads all global dependents of the application
    // such as app configurations in the web server
    this.store.dispatch(loadGlobal());
    this.actions.pipe(ofType(loadGlobalSuccess)).subscribe({
      next: (response) => {
        //this.router.navigate(['/main']);
      },
    });

    this.router.navigate(['/main/home']);
  }
}
