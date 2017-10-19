import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from "rxjs/Rx";

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';

@Component({
	selector: 'nav-bar',
	templateUrl: './navbar.component.html'
})
export class NavBarComponent {

	public user: any;

	public isAuthorized: boolean = false;

	constructor(
		private http: Http,
		@Inject('BASE_URL') private baseUrl: string) {

		http.get(baseUrl + "/api/identity/current")
			.subscribe(
			res => { },
			err => { this.unauthorized(); });
	}

	private unauthorized() {
		console.log("Unauthorized");

		this.isAuthorized = false;
	}
}