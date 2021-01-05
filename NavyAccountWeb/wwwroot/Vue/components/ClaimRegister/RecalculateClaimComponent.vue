<template>
    <!-- WRAPPER -->
    <div>

        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">�</span>
            </button>
        </div>
        <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
        <div class="card-body">

            <div class="row">
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Fund type</label>

                        <select class="form-control" v-model="postBody.FundTypeID" name="FundTypeID" required>
                            <option v-for="loantype in loantypeList" v-bind:value="loantype.code" v-bind:key="loantype.code"> {{ loantype.description }} </option>
                        </select>

                    </div>
                </div>
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Pending Claim</label>

                        <select class="form-control" v-model="postBody.PersonID" name="PersonID" @change="setValuePersonID()" required>
                            <option v-for="loantype in personList" v-bind:value="loantype.personID" v-bind:key="loantype.personID"> {{ loantype.svcno }} </option>
                        </select>

                    </div>
                </div>


                <div v-if="claimlist" class="card-body">
                    <div v-show="wantshow2">
                        <table id="datatables-buttons" class="table table-striped" style="width:100%">
                            <thead>
                                <tr>
                                    <th>Service No</th>
                                    <th>Total Contribution</th>
                                    <th>Additions</th>
                                    <th>Deduction</th>
                                    <th>Amount Due</th>
                                    <!-- <th></th>
                        <th></th> -->
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="discharges in claimlist">
                                    <td>{{discharges.svcno}}</td>
                                    <td>{{ discharges.totalContribution}}</td>
                                    <td>{{ discharges.interest }}</td>
                                    <td>{{ discharges.loan }}</td>
                                    <td>{{ discharges.amountDue }}</td>

                                    <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(discharges)">Continue</button></td>
                                </tr>
                            </tbody>


                        </table>
                    </div>
                    <div class="row" v-show="wantshow">

                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-label">Total Contribution</label>
                                <input class="form-control" name="description" v-model="postBody.totalContribution" readonly />
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-label">Additions</label>
                                <input class="form-control" name="description" v-model="postBody.interest" readonly />
                            </div>
                        </div>

                        <div class="col-md-4" v-if="postBody.amountReceived">
                            <div class="form-group">
                                <label class="form-label">Deductions</label>
                                <input class="form-control" name="description" v-model="postBody.loan" readonly />
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-label">Amount Due</label>
                                <input class="form-control" name="description" v-model="postBody.amountDue" readonly />

                            </div>
                        </div>

                        <!-- <div class="col-md-4">
                <div class="form-group">
                    <label class="form-label">Bank</label>
                    <select class="form-control" v-model="postBody.incomeacct" name="incomeacct" required>
                        <option v-for="liability in LiabilityList" v-bind:value="liability.acctcode" v-bind:key="liability.acctcode"> {{ liability.description }} </option>
                    </select>

                </div>
            </div> -->


                        <div class="row">
                            <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group">
                                <button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button>
                            </div>
                        </div>


                    </div>

                </div>

            </div>

        </div>
    </div>
    <!-- END WRAPPER -->
</template>

<script>
    import vuejsAutocomplete from 'vuejs-auto-complete'
    export default {
        components: {

            vuejsAutocomplete
        },
        props: ['fundtypeid'],
        data() {
            return {
                responseMessage: '',
                errors: null,
                searchData: '',
                submitorUpdate: 'submit',
                loantypeList: null,
                personList: null,
                LiabilityList: null,
                autoselectenabled: false,
                canProcess: true,
                claimlist: null,
                wantshow: false,
                wantshow2: true,
                claim: null,
                postBody: {
                    PersonID: 0,
                    FundTypeID: null,
                    amountPaid: 0,
                    amountReceived: 0,
                    amountDue: 0,
                    incomeacct: '',
                    totalContribution: 0,
                    loan: 0,
                    interest: 0
                },
            };
        },
        mounted() {
            this.$store.state.objectToUpdate = null,
                axios
                    .get('/api/ClaimRegister/GetClaimApproved')
                    .then(response => (this.personList = response.data))
            axios
                .get('/api/ChartofAccount/getChartofAccount4/4')
                .then(response => (this.LiabilityList = response.data))
            axios
                .get('/api/FundType/getAllFundTypes')
                .then(response => (this.loantypeList = response.data))
        },
        watch: {
            '$store.state.objectToUpdate': function (newVal, oldval) {
                this.postBody.amountReceived = this.$store.state.objectToUpdate.amountReceived,
                    this.postBody.amountDue = this.$store.state.objectToUpdate.amountDue,
                    this.postBody.totalContribution = this.$store.state.objectToUpdate.totalContribution,
                    this.postBody.amountPaid = this.$store.state.objectToUpdate.amountPaid,
                    this.postBody.FundTypeID = this.$store.state.objectToUpdate.fundTypeID,
                    this.postBody.PersonID = this.$store.state.objectToUpdate.personID,
                    this.postBody.interest = this.$store.state.objectToUpdate.interest
                this.submitorUpdate = 'Update';

            }
        },
        methods: {
            onChange: function () {
                alert(this.postBody.PersonID);

            },
            processRetrieve: function (discharges) {
                this.$store.state.objectToUpdate = discharges;
                this.wantshow = true;
                this.wantshow2 = false;
            },
            setValuePersonID(result) {

                axios.get(`/api/ClaimRegister/GetClaimApprovedBySvc/${this.postBody.PersonID}`)
                    .then(response => { this.claimlist = response.data })
            },
            checkForm: function (e) {
                if (this.postBody.totalContribution) {
                    e.preventDefault();
                    this.canProcess = false;
                    this.postPost();
                }
                else {

                    this.errors = [];
                    this.errors.push('Required');
                }
            },
            postPost() {
                if (this.submitorUpdate == 'Update') {
                    axios.post(`/api/ClaimRegister/UpdateReversal`, this.postBody)
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.postBody.PersonID = ''; this.postBody.appdate = '';
                                this.wanttoupdate = true;
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        },
        computed: {
            setter() {
                let objecttoedit = this.$store.state.objectToUpdate;

                if (objecttoedit.totalContribution) {
                    this.postBody.amountreceived = objecttoedit.amountReceived;
                    this.postBody.amountdue = objecttoedit.amountDue;
                    this.postBody.amountPaid = objecttoedit.amountPaid;
                    this.postBody.totalContribution = objecttoedit.totalContribution;
                    this.postBody.FundTypeID = objecttoedit.fundTypeID;
                    this.postBody.PersonID = objecttoedit.personID;
                    this.postBody.loan = objecttoedit.loan;
                    this.postBody.interest = objecttoedit.interest;
                }
            }
        }
    }

</script>
